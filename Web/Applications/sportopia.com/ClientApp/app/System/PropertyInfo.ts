import * as System from '../System/_include';

export class PropertyInfo {
	private __name: string;
	public get Name(): string {
		return this.__name;
	}
	public set Name(value: string) {
		this.__name = value;
	}
	private __propertyType: System.Type;
	public get PropertyType(): System.Type {
		return this.__propertyType;
	}
	public set PropertyType(value: System.Type) {
		this.__propertyType = value;
	}
	private __variable: string;
	public get Variable(): string {
		return this.__variable;
	}
	public set Variable(value: string) {
		this.__variable = value;
	}

	public GetValue(item: any) {
		return item[this.Name];
	}
	public SetValue(item: any, value: any) {
		item[this.Name] = value;
	}
	public GetVariable(item: any) {
		return item[this.Variable];
	}
	public SetVariable(item: any, value: any) {
		item[this.Variable] = value;
	}
}