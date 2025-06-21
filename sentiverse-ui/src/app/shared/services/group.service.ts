import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { Message } from '../models/message.model';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class GroupService {
  private hub?: HubConnection;
  private messagesSubject = new BehaviorSubject<Message[]>([]);
  messages$ = this.messagesSubject.asObservable();

  constructor(private http: HttpClient, private auth: AuthService) {}

  connect(groupId: string) {
    if (this.hub) {
      this.hub.stop();
    }
    this.hub = new HubConnectionBuilder()
      .withUrl(`/hubs/emotionGroup?groupId=${groupId}`, {
        accessTokenFactory: () => this.auth.getToken() || ''
      })
      .withAutomaticReconnect()
      .build();

    this.hub.on('ReceiveMessage', (message: Message) => {
      this.messagesSubject.next([...this.messagesSubject.value, message]);
    });

    return this.hub.start();
  }

  loadMessages(groupId: string) {
    return this.http
      .get<Message[]>(`/api/groups/${groupId}/messages`)
      .subscribe(msgs => this.messagesSubject.next(msgs));
  }

  sendMessage(groupId: string, content: string) {
    return this.hub?.invoke('SendMessage', { groupId, content });
  }
}
