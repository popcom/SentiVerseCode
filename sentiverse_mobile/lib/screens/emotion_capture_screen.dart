import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/auth_provider.dart';
import '../providers/chat_provider.dart';
import '../services/api_service.dart';
import 'chat_screen.dart';
import '../widgets/emotion_icon.dart';

class EmotionCaptureScreen extends StatefulWidget {
  const EmotionCaptureScreen({super.key});

  @override
  State<EmotionCaptureScreen> createState() => _EmotionCaptureScreenState();
}

class _EmotionCaptureScreenState extends State<EmotionCaptureScreen> {
  final _controller = TextEditingController();
  final ApiService _api = ApiService();
  String? _emotion;
  bool _loading = false;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Capture Emotion'),
        actions: [
          IconButton(
              onPressed: () {
                context.read<AuthProvider>().logout();
                Navigator.pushReplacementNamed(context, '/login');
              },
              icon: const Icon(Icons.logout))
        ],
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          children: [
            TextField(
              controller: _controller,
              decoration:
                  const InputDecoration(labelText: 'How do you feel right now?'),
            ),
            const SizedBox(height: 20),
            _loading
                ? const CircularProgressIndicator()
                : ElevatedButton(
                    onPressed: () async {
                      if (_controller.text.isEmpty) return;
                      setState(() => _loading = true);
                      final res = await _api.post('/api/emotions/capture', {
                        'text': _controller.text,
                      });
                      setState(() => _loading = false);
                      if (res.statusCode == 200) {
                        final data = jsonDecode(res.body);
                        setState(() {
                          _emotion = data['emotion'];
                        });
                      } else {
                        if (!mounted) return;
                        ScaffoldMessenger.of(context).showSnackBar(
                            const SnackBar(content: Text('Failed to detect emotion')));
                      }
                    },
                    child: const Text('Detect Emotion'),
                  ),
            const SizedBox(height: 20),
            if (_emotion != null) ...[
              EmotionIcon(emotion: _emotion!),
              Text(_emotion!, style: Theme.of(context).textTheme.headline6),
              const SizedBox(height: 10),
              ElevatedButton(
                onPressed: () async {
                  context.read<ChatProvider>().joinGroup(_emotion!);
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                        builder: (_) => ChatScreen(groupId: _emotion!)),
                  );
                },
                child: const Text('Join Group'),
              ),
            ]
          ],
        ),
      ),
    );
  }
}
