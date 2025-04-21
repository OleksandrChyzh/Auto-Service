import { Component, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';

interface User {
  name: string;
  email: string;
  avatarUrl?: string;
}

@Component({
  selector: 'app-user-profile-dropdown',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './user-profile-dropdown.component.html',
  styleUrl: './user-profile-dropdown.component.css'
})
export class UserProfileDropdownComponent {
  isDropdownOpen = false;

  user: User = {
    name: 'Олександр Чиж',
    email: 'Oleksandr.Chyzh.PZ.2022@edu.lpnu.ua',
    // avatarUrl: 'url-to-avatar' // Якщо буде аватар
  };

  constructor(private router: Router) {}

  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  @HostListener('document:click', ['$event'])
  closeDropdown(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.user-dropdown')) {
      this.isDropdownOpen = false;
    }
  }

  getInitials(): string {
    return this.user.name
      .split(' ')
      .map(name => name.charAt(0))
      .join('')
      .toUpperCase()
      .substring(0, 2);
  }

  logout(): void {
    // Очистити дані сесії, якщо треба
    // localStorage.removeItem('token');

    console.log('Вихід з системи');

    // Перехід на головну сторінку
    this.router.navigate(['/']);
  }
}
