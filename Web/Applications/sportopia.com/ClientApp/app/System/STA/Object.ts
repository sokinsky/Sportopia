import * as System from "../_include";
import * as STA from './_include';

export class Object {
	//STA.Object
	constructor(context: STA.Context, init?: any) {
        this._context = context;
		this.Update(init);
	}
	public _context: STA.Context;
	public static readonly _type:any = {
		"Name": "STA.Object",
		"Properties": []
	};
	public GetType(): System.Type {
		return System.Type.GetType(STA.Object._type.Name);
	}


	public Update(value?: any) {
		for (var propertyName in value) {
			console.log(propertyName);
			let propertyInfo: any = this.GetType().GetProperty(propertyName);
			if (propertyInfo)
				this.update_property(propertyInfo, value[propertyName]);			
		}
	}
	protected update_property(propertyInfo: System.PropertyInfo, value: any) {
		if (propertyInfo) {
			var propertyValue = this.GetPropertyValue(propertyInfo)
			if (propertyInfo.PropertyType.IsSubClassOf('STA.Object')) {
				let constructor: any = propertyInfo.PropertyType.GetConstructor();
				propertyInfo.SetValue(this, new constructor());
			}
			else {
				propertyInfo.SetValue(this, value);
			}
		}
	}

	public GetPropertyValue(propertyInfo: System.PropertyInfo): any{
        if (typeof (propertyInfo) == 'string') 
            propertyInfo = this.GetType().GetProperty(propertyInfo);
		if (!propertyInfo)
            throw propertyInfo + ' could not be found';
		return this[propertyInfo.Name];
	}
    public SetPropertyValue(propertyInfo: System.PropertyInfo, value:any): any {
        if (typeof (propertyInfo) == 'string')
            propertyInfo = this.GetType().GetProperty(propertyInfo);
        if (!propertyInfo)
            throw propertyInfo + ' could not be found';
		return this[propertyInfo.Name] = value
	}
}