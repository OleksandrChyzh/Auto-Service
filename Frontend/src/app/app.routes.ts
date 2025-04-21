import { Routes } from '@angular/router';
import {ContactsComponent} from './pages/main_page/contacts/contacts.component';
import {HomeComponent} from './pages/main_page/home/home.component';
import {AutoServicesComponent} from './pages/main_page/auto-services/auto-services.component';
import {ClientPageComponent} from './pages/client-page/client-page.component';
import {MasterPageComponent} from './pages/master-page/master-page.component';
import {AdminPageComponent} from './pages/admin-page/admin-page.component';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'contacts', component: ContactsComponent },
  {path: 'auto-services', component: AutoServicesComponent},
  {path: 'app-client-page', component:ClientPageComponent},
  {path: 'app-master-page', component: MasterPageComponent},
  {path: 'app-admin-page', component: AdminPageComponent},
];
