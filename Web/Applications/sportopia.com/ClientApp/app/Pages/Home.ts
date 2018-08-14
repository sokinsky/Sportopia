import { Component } from '@angular/core';
import * as System from '../System/_include';
import * as STA from '../System/STA/_include';

@Component({
	selector: 'home',
	templateUrl: './Home.html'
})
export class Home {
	constructor(private sta:STA.Context) {
		
	}
	public Test() {
		let person: STA.Data.Models.Person = new STA.Data.Models.Person(this.sta);
		person.Update({ Name: "Steve" });
		console.log(person);
		let propertyInfo: System.PropertyInfo = person.GetType().GetProperty("Name");
		//console.log(System.Type.GetType("STA.Data.Models.Person").GetProperties());


		//console.log(person);
	}
}
