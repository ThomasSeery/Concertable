import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { HeaderType } from '../../models/header-type';
import { ActivatedRoute } from '@angular/router';
import { Genre } from '../../models/genre';
import { GenreService } from '../../services/genre/genre.service';
import { SearchParams } from '../../models/search-params';
import { CustomerFindComponent } from '../customer-find/customer-find.component';

@Component({
  selector: 'app-filter',
  standalone: false,
  templateUrl: './filter.component.html',
  styleUrls: ['./filter.component.scss']
})
export class FilterComponent implements OnInit {
  @Input() headerType?: HeaderType;
  @Input() searchParams!: SearchParams;
  @Output() headerTypeChange = new EventEmitter<HeaderType | undefined>();
  @Output() searchParamsChange = new EventEmitter<SearchParams>();

  icon: string = 'tune';
  headerTypes: HeaderType[] = ['venue', 'artist', 'event'];
  genres: Genre[] = [];
  selectedGenre?: Genre;
  selectedGenres: Genre[] = [];
  selectedOrderBy?: string;
  selectedSortOrder?: string;
  selectedDistance: number =0;

  get orderByOptions() {
    const options = ['Name']; 
    if (this.headerType === 'event') 
      options.push('Date'); 
    if (this.searchParams.latitude && this.searchParams.longitude) 
      options.push('Location');
    return options;
  }
  
  get locationSelected(): boolean {
    return Boolean(this.searchParams.latitude && this.searchParams.longitude)
  }

  onOrderByChange() {
    if(this.selectedOrderBy) 
    {
      this.selectedSortOrder = this.selectedSortOrder || 'asc';
      this.searchParams.sort = `${this.selectedOrderBy}_${this.selectedSortOrder}`;
    }
    else 
      this.selectedSortOrder = undefined; // Reset sort order if Order By is deselected
  }

  onSortOrderChange() {
    if(this.selectedOrderBy)
      this.searchParams.sort = `${this.selectedOrderBy}_${this.selectedSortOrder}`;
  }

  onDistanceChange() {
    console.log("change",this.selectedDistance)
    this.searchParams.radiusKm = this.selectedDistance;
  }

  constructor(private route: ActivatedRoute, private genreService: GenreService) {}

  ngOnInit(): void {
    this.getGenres();
  }

  getGenres() {
    this.genreService.getAll().subscribe(g => {
      this.genres = g;
      this.loadSelectedGenres();
    });
  }

  loadSelectedGenres() {
    console.log("genres",this.genres);
    console.log("ids",this.searchParams.genreIds);
    this.searchParams.genreIds?.forEach(id => {
      const genre = this.genres.find(g => g.id === id);
      if (genre) {
        this.selectedGenres.push(genre);
      }
    });

    console.log("selectedgenres",this.selectedGenres);
  }
  

  private emitChanges() {
    const genreIds = this.selectedGenres.map(g => g.id);
    this.searchParams.genreIds = genreIds;
    this.searchParamsChange.emit(this.searchParams);
  }

  isCustomerRoute(): boolean {
    return this.route.component === CustomerFindComponent;
  }

  onHeaderTypeChange() {
    this.headerTypeChange.emit(this.headerType);
  }

  onGenreChange() {
    console.log(this.selectedGenre?.name);
  }

  addGenre() {
    if (this.selectedGenre && !this.selectedGenres.includes(this.selectedGenre)) {
      this.selectedGenres.push(this.selectedGenre);
      this.emitChanges();
    }
  }

  removeGenre(genre: Genre) {
    this.selectedGenres = this.selectedGenres.filter(g => g !== genre);
    this.emitChanges();
  }
}
