import * as System from "../../_include";
import * as STA from "../_include";

// STA.API.Request
export class Request extends STA.Object {
	//STA.Error
	constructor(assembly: STA.Context, init?: any) {
		super(assembly, init);
	}

	private __url: string;
	public get Url(): string{
		return this.__url;
	}
	public set Url(value: string){
		this.__url = value;
	}

	private __result: any;
	public get Result(): any {
		return this.__result;
	}
	public set Result(value: any) {
		this.__result = value;
	}
	public Error: any;
}
