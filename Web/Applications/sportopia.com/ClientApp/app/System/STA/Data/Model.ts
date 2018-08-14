import * as System from "../../_include";
import * as STA from "../_include";
import * as Base from "../Object";


export class Model extends Base.Object {
	// STA.Data.Model
	constructor(assembly: STA.Context, init?: any) {
		super(assembly, init);
	}

	private __iD: number;
	public get ID(): number { return this.__iD; }
	public set ID(value: number) { this.__iD = value; }

	public static readonly _type:any = {
		"Name": "STA.Data.Model",
		"BaseType":"STA.Object",
		"Properties": [
			{
				"Name": "ID",
				"PropertyType": "number",
				"Variable": "__iD"
			}
		]
	};	
	public GetType(): any {
		return System.Type.GetType(STA.Data.Model._type.Name);
	}

	////public static Select(context: STA.Data.Context, type: any, value: any, result?: any): Observable<STA.API.Response> {
	////	if (typeof (type) == "string") {
	////		if (type.indexOf("STA") != 0)
	////			type = "STA.Data.Models." + type;
	////		type = System.Type.GetType(context, type);
	////	}
	////	let response: Observable<STA.API.Response> = STA.API.Model.Select.Request.Send(context, type, value, result);
	////	return response;
	////}

	//protected update_property(propertyInfo: any, value: any) {
	//	if (propertyInfo) {
	//		if (!propertyInfo.Type.IsSubTypeOf("System.STA.Data.Model")) {
	//			super.update_property(propertyInfo, value);
	//			return;
	//		}
	//		var privateName = "__" + propertyInfo.Name.substring(0, 1).toLowerCase() + propertyInfo.Name.substring(1);
	//		let object: any = this;
	//		var currentValue = object[privateName];
	//		if (currentValue == null) {
	//			currentValue = propertyInfo.Type.GetConstructor()();
	//			object[privateName] = currentValue;
	//		}
	//		currentValue.Update(propertyInfo, value);
	//	}
	//}

	//public GetProperty(name: any): any {
	//	let property: any;
	//	if (typeof (name) == "string")
	//		property = this.GetType().GetProperty(name);
	//	else
	//		property = name;

	//	var privateName = "__" + property.Name.substring(0, 1).toLowerCase() + property.Name.substring(1);
	//	let object: any = this;		
	//	//if (object[privateName] === undefined) {
	//	//	object[privateName] = null;
	//	//	STA.API.Model.Property.Request.Send(this, property).subscribe((response) => {
	//	//		object[privateName] = response.Result;
	//	//		console.log(this);
	//	//	});
	//	//}
	//	return object[privateName];
	//}
	//public SetProperty(name: any, value: any) {
	//	//let property: System.Property;
	//	//if (typeof (name) == "string")
	//	//	property = this.GetType().GetProperty(name);
	//	//else
	//	//	property = name;

	//	//var privateName = "__" + property.Name.substring(0, 1).toLowerCase() + property.Name.substring(1);
	//	//let model: any = this;
	//	//if (!value) {
	//	//	model[privateName] = value;
	//	//	return;
	//	//}	
	//	//if (!property.Type.IsSubTypeOf("System.STA.Data.Model")) {
	//	//	super.SetProperty(property, value);
	//	//	return;
	//	//}
	//	//if (value.GetType) {
	//	//	if (value.GetType() == property.Type)
	//	//		model[privateName] = value;
	//	//	else
	//	//		throw "SetProperty failed: value(" + value.GetType().FullName + ") does not match the propertyType(" + property.Type.FullName + ")";
	//	//}
	//	//else {
	//	//	var newValue = null;
	//	//	if (value.ID)
	//	//		newValue = this.Context.Select(property.Type, value.ID);

	//	//	if (!newValue) {
	//	//		newValue = property.Type.GetConstructor()(this.Context);
	//	//		this.Context.Add(newValue);
	//	//	}
	//	//	newValue.Update(this.Context, value);				
	//	//}
	//}
}