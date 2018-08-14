import * as System from "../../../../_include";
import * as STA from "../../../_include";

export class RequestModel extends STA.Object {
	//STA.Data.Requests.Model.RequestModel
	constructor(assembly: STA.Context, init?: any) {
		super(assembly, init);
    }

    private __type: string;
    public get Type(): string {
        return this.__type;
    }
    public set Type(value: string) {
        this.__type = value;
    }
    private __value: any;
    public get Value(): any {
        return this.__value;
    }
    public set Value(value: any) {
        this.__value = value;
    }

    public static readonly _type: any = {
        "Name": "STA.Data.Requests.Model.RequestModel",
        "BaseType": "STA.Object",
        "Properties": [
            {
                "Name": "Type",
                "PropertyType": "string",
                "Variable": "__type"
            },
            {
                "Name": "Value",
                "PropertyInfo": "any",
				"Variable":"__value"
            }
        ]
    };
    public GetType(): any {
        return System.Type.GetType(STA.Data.Model._type.Name);
    }
}
