import * as System from '../../_include';
import * as STA from '../_include';

export class Context {
	constructor(assembly: STA.Context) {
        this._assembly = assembly;
	}
	private _assembly: STA.Context;
}