import * as System from '../_include';
import * as LMS from './_include';

export class Object {
	//LMS.Object
	constructor(assembly: LMS.Context, init?: any) {
		this._assembly = assembly;
		this.Update(init);
	}
	public _assembly: LMS.Context;
	public static readonly _type: any = {
		"Name": "LMS.Object",
		"Properties": []
	};
	public GetType(): any {
		return System.Type.GetType(LMS.Object._type.Name);
	}


	public Update(value?: any) {
		for (var propertyName in value) {
			let propertyInfo: any = this.GetType().GetProperty(propertyName);
			if (propertyInfo)
				this.update_property(propertyInfo, value[propertyName]);			
		}
	}
	protected update_property(propertyInfo: any, value: any) {
		if (propertyInfo) {
			if (propertyInfo.PropertyType.IsSubClassOf("LMS.Object")) {
				let constructor: any = propertyInfo.PropertyType.GetConstructor();
				propertyInfo.SetValue(this, new constructor());
			}
			else {
				propertyInfo.SetValue(this, value);
			}
		}
	}
}