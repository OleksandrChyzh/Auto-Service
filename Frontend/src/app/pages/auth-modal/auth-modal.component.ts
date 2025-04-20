import { Component, EventEmitter, Output } from '@angular/core';
@Component({
  selector: 'app-auth-modal',
  imports: [],
  templateUrl: './auth-modal.component.html',
  styleUrl: './auth-modal.component.css'
})
export class AuthModalComponent {
  @Output() close = new EventEmitter<void>();
  activeTab = 'login';

  closeModal(): void {
    this.close.emit();
  }

  setActiveTab(tab: 'login' | 'register'): void {
    this.activeTab = tab;
  }

  login(event: Event): void {
    event.preventDefault();
    // Тут код для авторизації
    console.log('Логін...');
    // Після успішного входу закриваємо модальне вікно
    this.closeModal();
  }

  register(event: Event): void {
    event.preventDefault();
    // Тут код для реєстрації
    console.log('Реєстрація...');
    // Після успішної реєстрації закриваємо модальне вікно
    this.closeModal();
  }
}
