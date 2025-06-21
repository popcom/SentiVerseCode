import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import '../utils/constants.dart';

class ApiService {
  final _storage = const FlutterSecureStorage();

  Future<String?> _getToken() async => await _storage.read(key: 'jwt');

  Future<http.Response> post(String path, Map<String, dynamic> body) async {
    final token = await _getToken();
    final response = await http.post(
      Uri.parse('${Constants.apiBaseUrl}' + path),
      headers: {
        'Content-Type': 'application/json',
        if (token != null) 'Authorization': 'Bearer $token',
      },
      body: jsonEncode(body),
    );
    return response;
  }

  Future<http.Response> get(String path) async {
    final token = await _getToken();
    final response = await http.get(
      Uri.parse('${Constants.apiBaseUrl}' + path),
      headers: {
        if (token != null) 'Authorization': 'Bearer $token',
      },
    );
    return response;
  }
}
