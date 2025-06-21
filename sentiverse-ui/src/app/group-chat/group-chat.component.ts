import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { GroupService } from '../shared/services/group.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-group-chat',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './group-chat.component.html',
  styleUrl: './group-chat.component.scss'
})
export class GroupChatComponent implements OnInit, OnDestroy {
  groupId!: string;
  messages: any[] = [];
  message = '';
  private sub?: Subscription;

  constructor(private route: ActivatedRoute, private group: GroupService) {}

  ngOnInit() {
    this.groupId = this.route.snapshot.paramMap.get('groupId')!;
    this.group.loadMessages(this.groupId);
    this.group.connect(this.groupId);
    this.sub = this.group.messages$.subscribe(m => (this.messages = m));
  }

  send() {
    if (!this.message) return;
    this.group.sendMessage(this.groupId, this.message);
    this.message = '';
  }

  ngOnDestroy() {
    this.sub?.unsubscribe();
  }
}
