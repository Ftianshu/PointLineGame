shader_type canvas_item;


uniform float alpha;
void fragment() {
	// Place fragment code here.
	COLOR = texture(TEXTURE, UV) * vec4(1.,1.,1.,alpha);
}
