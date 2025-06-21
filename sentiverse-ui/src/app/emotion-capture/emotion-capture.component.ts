import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

interface EmotionResponse { emotion: string; groupId: string; }

@Component({
  selector: 'app-emotion-capture',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './emotion-capture.component.html',
  styleUrl: './emotion-capture.component.scss'
})
export class EmotionCaptureComponent {
  result?: EmotionResponse;
  form: FormGroup;

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router) {
    this.form = this.fb.group({
      text: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.invalid) return;
    this.http.post<EmotionResponse>('/api/emotions/capture', this.form.value).subscribe(res => this.result = res);
  }

  join() {
    if (this.result) {
      this.router.navigate(['/group', this.result.groupId]);
    }
  }
}
