import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthModalComponent } from '../../../pages/auth-modal/auth-modal.component'; // правильний шлях
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-top-panel',
  standalone: true,
  imports: [
    RouterLink,
    CommonModule,
    AuthModalComponent  // Додали компонент AuthModal
  ],
  templateUrl: './top-panel.component.html',
  styleUrl: './top-panel.component.css'
})
export class TopPanelComponent {
  showAuthModal = false;

  openAuthModal(): void {
    this.showAuthModal = true;
  }

  closeAuthModal(): void {
    this.showAuthModal = false;
  }
}
