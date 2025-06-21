import 'package:flutter/material.dart';
import 'package:provider/provider.dart';
import '../providers/chat_provider.dart';
import '../providers/auth_provider.dart';
import '../widgets/chat_bubble.dart';
import '../widgets/emotion_icon.dart';

class ChatScreen extends StatefulWidget {
  final String groupId;
  const ChatScreen({super.key, required this.groupId});

  @override
  State<ChatScreen> createState() => _ChatScreenState();
}

class _ChatScreenState extends State<ChatScreen> {
  final _controller = TextEditingController();

  @override
  void initState() {
    super.initState();
    context.read<ChatProvider>().joinGroup(widget.groupId);
  }

  @override
  void dispose() {
    context.read<ChatProvider>().leaveGroup();
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final messages = context.watch<ChatProvider>().messages;
    final userId = context.read<AuthProvider>().token; // decode if needed
    return Scaffold(
      appBar: AppBar(
        title: Row(
          children: [
            EmotionIcon(emotion: widget.groupId),
            const SizedBox(width: 8),
            Text(widget.groupId),
          ],
        ),
      ),
      body: Column(
        children: [
          Expanded(
            child: ListView.builder(
              itemCount: messages.length,
              itemBuilder: (context, index) {
                final msg = messages[index];
                return ChatBubble(
                  message: msg.content,
                  isMe: msg.userId == userId,
                );
              },
            ),
          ),
          Padding(
            padding: const EdgeInsets.all(8.0),
            child: Row(
              children: [
                Expanded(
                  child: TextField(
                    controller: _controller,
                    decoration: const InputDecoration(hintText: 'Message'),
                  ),
                ),
                IconButton(
                  icon: const Icon(Icons.send),
                  onPressed: () {
                    final text = _controller.text.trim();
                    if (text.isEmpty) return;
                    context
                        .read<ChatProvider>()
                        .sendMessage(widget.groupId, text);
                    _controller.clear();
                  },
                )
              ],
            ),
          )
        ],
      ),
    );
  }
}
