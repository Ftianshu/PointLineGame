shader_type canvas_item;
uniform bool mouseIn;

void fragment() {
	// Place fragment code here.
	if(mouseIn){
		COLOR = vec4(0.6, 0.6, 0.6, 0.8);
	}else{
		COLOR = vec4(0.1, 0.1, 0.1, 0.8);
	}
}
