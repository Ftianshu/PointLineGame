shader_type canvas_item;

uniform float alpha;
void fragment() {
	// Place fragment code here.
	COLOR = vec4(vec3(1.0), alpha);
}
