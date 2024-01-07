shader_type canvas_item;
uniform int rarity;

void fragment() {
	
	// Place fragment code here.
		switch(rarity){
			case 0:{
				COLOR = vec4(0.1, 0.1, 0.1, 0.8);
				break;
			}
			case 1:{
				COLOR = vec4(86.0 / 255.0, 152.0 / 255.0, 195.0 / 255.0, 0.8);
				break;
			}
			case 2:{
				COLOR = vec4(68.0 / 255.0, 14.0 / 255.0, 37.0 / 255.0, 0.8);
				break;
			}
			case 3:{
				COLOR = vec4(222.0 / 255.0, 42.0 / 255.0, 24.0 / 255.0, 0.8);
				break;
			}
	}
}
