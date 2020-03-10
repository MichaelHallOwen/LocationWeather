import { Component, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { InjectionToken } from '@angular/core';


@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})
export class HomeComponent {
    public weather: Weather;

    constructor(private http: Http) {
    }
  

    public getWeather(chosenCity: string) {
        this.http.get('/api/weather/city/' + chosenCity).subscribe(result => {
            this.weather = result.json();
        });
    }
}

interface Weather {
    temp: string;
    summary: string;
    city: string;
}

