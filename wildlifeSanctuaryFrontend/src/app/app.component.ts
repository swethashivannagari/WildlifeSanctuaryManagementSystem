import { HttpClientModule } from '@angular/common/http';
import Chart from 'chart.js/auto'
import { Component } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/navbar/navbar.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [NavbarComponent, RouterOutlet,ReactiveFormsModule,HttpClientModule,
 ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'wildlifeSanctuaryFrontend';
}
