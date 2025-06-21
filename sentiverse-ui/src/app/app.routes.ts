import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { EmotionCaptureComponent } from './emotion-capture/emotion-capture.component';
import { GroupChatComponent } from './group-chat/group-chat.component';
import { authGuard } from './shared/guards/auth.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'emotion', component: EmotionCaptureComponent, canActivate: [authGuard] },
  { path: 'group/:groupId', component: GroupChatComponent, canActivate: [authGuard] },
  { path: '**', redirectTo: 'login' }
];
