import * as System from "../_include";
import * as STA from './_include';

export class Error extends STA.Object {
	//STA.Error
	constructor(assembly: STA.Context, init?: any) {
		super(assembly, init);
	}

	private __name: string;
	public get Name(): string {
		return this.__name;
	}
	public set Name(value: string) {
		this.__name = value;
	}
	private __message: string;
	public get Message(): string {
		return this.__message;
	}
	public set Message(value: string) {
		this.__message = value;
	}
	private __description: string;
	public get Description(): string {
		return this.__description;
	}
	public set Description(value: string) {
		this.__description = value;
	}
	private __innerErrors: Array<Error>;
	public get InnerErrors(): Array<Error> {
		return this.__innerErrors;
	}
	public set InnerErrors(value: Array<Error>) {
		this.__innerErrors = value;
	}

	public static readonly _type: any = {
		"Name": "STA.Error",
		"Properties": [
			{
				"Name": "Name",
				"PropertyType": "string",
				"Variable": "__name"
			},
			{
				"Name": "Message",
				"PropertyType": "string",
				"Variable": "__message"
			},
			{
				"Name": "Description",
				"PropertyType": "string",
				"Variable": "__description"
			},
			{
				"Name": "InnerErrors",
				"PropertyType": "Array<STA.Error>",
				"Variable": "__innerErrors"
			},
		]
	};
	public GetType(): System.Type {
		return System.Type.GetType(STA.Object._type.Name);
	}


}