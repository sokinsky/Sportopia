import * as Base from '../AssemblyInfo';
import * as STA from './_include';

import { Injectable } from '@angular/core';


@Injectable()
export class Assembly extends Base.AssemblyInfo {
	constructor() {		
		super();
		this.__name = "STA";
	}
	public GetType(name: string) {
		return this.getType(STA, name);
	}

}