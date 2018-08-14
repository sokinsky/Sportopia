import * as System from "../../../../_include";
import * as STA from "../../../_include";

// STA.API.Request
export class Request extends STA.Data.Request {
	//STA.Error
	constructor(assembly: STA.Context, init?: any) {
		super(assembly, init);
    }

    private __model: STA.Data.Requests.Model.RequestModel;
    public get Model(): STA.Data.Requests.Model.RequestModel {
        return this.__model;
    }
    public set Model(value: STA.Data.Requests.Model.RequestModel) {
        this.__model = value;
    }

    public static readonly _type: any = {
        "Name": "STA.Data.Requests.Model.Request",
        "BaseType": "STA.Data.Request",
        "Properties": [
            {
                "Name": "Model",
                "PropertyType": "STA.Data.Requests.Model.RequestModel",
                "Variable": "__model"
            }
        ]
    };
    public GetType(): any {
        return System.Type.GetType(STA.Data.Model._type.Name);
    }
}
