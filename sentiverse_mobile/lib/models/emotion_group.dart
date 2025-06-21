class EmotionGroup {
  final String id;
  final String emotion;

  EmotionGroup({required this.id, required this.emotion});

  factory EmotionGroup.fromJson(Map<String, dynamic> json) {
    return EmotionGroup(
      id: json['id'] ?? '',
      emotion: json['emotion'] ?? '',
    );
  }
}
