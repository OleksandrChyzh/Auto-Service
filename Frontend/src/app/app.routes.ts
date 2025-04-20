import { Routes } from '@angular/router';
import {ContactsComponent} from './pages/main_page/contacts/contacts.component';
import {HomeComponent} from './pages/main_page/home/home.component';
import {AutoServicesComponent} from './pages/main_page/auto-services/auto-services.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contacts', component: ContactsComponent },
  {path: 'auto-services', component: AutoServicesComponent}// ← додаємо
];
