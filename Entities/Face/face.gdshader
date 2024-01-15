shader_type canvas_item;

uniform float alpha;
void fragment() {
	// Place fragment code here.
	COLOR = vec4(vec3(124.0 / 255.0,63.0 / 255.0,88.0 / 255.0), alpha);
}
