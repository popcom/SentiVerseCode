import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import 'providers/auth_provider.dart';
import 'providers/chat_provider.dart';
import 'screens/login_screen.dart';
import 'screens/register_screen.dart';
import 'screens/emotion_capture_screen.dart';

void main() {
  runApp(const SentiVerseApp());
}

class SentiVerseApp extends StatelessWidget {
  const SentiVerseApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MultiProvider(
      providers: [
        ChangeNotifierProvider(create: (_) => AuthProvider()..loadToken()),
        ChangeNotifierProvider(create: (_) => ChatProvider()),
      ],
      child: Consumer<AuthProvider>(
        builder: (context, auth, _) {
          return MaterialApp(
            title: 'SentiVerse',
            theme: ThemeData.dark(useMaterial3: true),
            routes: {
              '/login': (_) => const LoginScreen(),
              '/register': (_) => const RegisterScreen(),
              '/emotion': (_) => const EmotionCaptureScreen(),
            },
            home: auth.isAuthenticated
                ? const EmotionCaptureScreen()
                : const LoginScreen(),
          );
        },
      ),
    );
  }
}
