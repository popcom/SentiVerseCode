import 'dart:convert';

import '../models/user.dart';
import '../utils/constants.dart';
import 'api_service.dart';

class AuthService {
  final ApiService _api = ApiService();

  Future<String?> login(String email, String password) async {
    final res = await _api.post('/api/auth/login', {
      'email': email,
      'password': password,
    });
    if (res.statusCode == 200) {
      final data = jsonDecode(res.body);
      return data['token'];
    }
    return null;
  }

  Future<bool> register(String username, String email, String password) async {
    final res = await _api.post('/api/auth/register', {
      'username': username,
      'email': email,
      'password': password,
    });
    return res.statusCode == 200;
  }
}
