import 'dart:async';
import 'dart:convert';
import 'package:flutter/material.dart';

import '../models/message.dart';
import '../services/api_service.dart';
import '../services/signalr_service.dart';

class ChatProvider with ChangeNotifier {
  final ApiService _api = ApiService();
  final SignalRService _signalR = SignalRService();
  List<Message> _messages = [];
  StreamSubscription? _sub;

  List<Message> get messages => _messages;

  Future<void> joinGroup(String groupId) async {
    await _signalR.connect(groupId);
    _sub = _signalR.on('ReceiveMessage')?.listen((data) {
      final msg = Message.fromJson(Map<String, dynamic>.from(data[0]));
      _messages.add(msg);
      notifyListeners();
    });
    final res = await _api.get('/api/groups/$groupId/messages');
    if (res.statusCode == 200) {
      final list = jsonDecode(res.body) as List;
      _messages = list.map((e) => Message.fromJson(e)).toList();
      notifyListeners();
    }
  }

  Future<void> sendMessage(String groupId, String content) async {
    await _signalR.send('SendMessage', [groupId, content]);
  }

  Future<void> leaveGroup() async {
    await _sub?.cancel();
    await _signalR.disconnect();
    _messages = [];
  }
}
