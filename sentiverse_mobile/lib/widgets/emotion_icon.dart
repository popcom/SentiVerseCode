import 'package:flutter/material.dart';

class EmotionIcon extends StatelessWidget {
  final String emotion;
  const EmotionIcon({super.key, required this.emotion});

  @override
  Widget build(BuildContext context) {
    String emoji;
    switch (emotion.toLowerCase()) {
      case 'happy':
        emoji = '😊';
        break;
      case 'sad':
        emoji = '😢';
        break;
      case 'angry':
        emoji = '😠';
        break;
      default:
        emoji = '🙂';
    }
    return Text(
      emoji,
      style: const TextStyle(fontSize: 24),
    );
  }
}
