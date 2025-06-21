import 'package:signalr_core/signalr_core.dart';

class SignalRService {
  HubConnection? _connection;

  Future<void> connect(String groupId) async {
    _connection = HubConnectionBuilder()
        .withUrl('https://your-api.com/hubs/emotionGroup?groupId=$groupId')
        .withAutomaticReconnect()
        .build();
    await _connection!.start();
  }

  Stream<dynamic>? on(String method) => _connection?.on(method);

  Future<void> send(String method, List<Object?> args) async {
    if (_connection?.state == HubConnectionState.connected) {
      await _connection!.invoke(method, args);
    }
  }

  Future<void> disconnect() async {
    await _connection?.stop();
  }
}
