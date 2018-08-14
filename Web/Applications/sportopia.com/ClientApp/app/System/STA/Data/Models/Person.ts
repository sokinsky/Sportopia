import * as System from "../../../_include";
import * as STA from "../../../STA/_include";
import * as Base from "../Model";

export class Person extends Base.Model {
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

	public static readonly _type: any = {
		"Name": "STA.Data.Models.Person",
		"BaseType": "STA.Data.Model",
		"Properties": [
			{
				"Name": "Name",
				"PropertyType": "string",
				"Variable": "__name"
			}
		]
	};
	public GetType(): any {
		return System.Type.GetType(STA.Data.Models.Person._type.Name);
	}
}