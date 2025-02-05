import { Component, Input } from '@angular/core';
import { Header } from '../../models/header';
import { ArtistHeader } from '../../models/artist-header';

@Component({
  selector: 'app-artist-header',
  standalone: false,
  
  templateUrl: './artist-header.component.html',
  styleUrl: './artist-header.component.scss'
})
export class ArtistHeaderComponent {
  @Input() header?: ArtistHeader;
}
