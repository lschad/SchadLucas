let ClassName = "Icons";
let FileName = `${ClassName}.cs`;
let Source = __dirname + "/../FontAwesome/advanced-options/metadata/icons.json";
let Target = __dirname + `/../${FileName}`;

function GenerateIcons(className, sourceFile, targetFile) {
	const fs = require("fs");
	const path = require("path");

	function onError(error) {
		console.error("Unexpected Error");
		console.error(error);
	}

	function normalizeIcon(icon) {

		if (icon.match(/^\d/)) {
			icon = `_${icon}`; 
		}

		icon = icon.replace(/-([a-z])/g, function (g) { return g[1].toUpperCase(); });
		icon = icon.replace(/^\w/, function (chr) {
			return chr.toUpperCase();
		});
		return icon;

	}

	fs.readFile(sourceFile, { encoding: "utf-8" }, (err, json) => {

		if (err) {
			onError(err);
			return;
		}

		var awesome = JSON.parse(json);
		var linebreak = `\r\n`;

		function write(tabs, string) {
			let out = "";
			for (let i = 0; i < tabs; i++) {
				out += "\t";
			}
			out += string;
			out += linebreak;
			return out;
		}

		function fileHeader() {
			console.info("Build Header");
			let header = write(0, "// ReSharper disable All");
			header += write(0, "namespace SchadLucas.Wpf.FontAwesome");
			header += write(0, "{");
			header += write(1, `public static class ${className}`);
			header += write(1, `{`);
			return header;
		}

		function fileBottom() {
			console.info("Build Bottom");
			
			let bottom = write(1, "}");
			bottom += write(0, "}");


			return bottom;
		}

		function buildEnum(name) {
			console.info(`Build Enum ${name}`);

			let cs = write(2, `public enum  ${name}`);
			cs += write(2, `{`);

			for (let icon in awesome) {
				if (awesome.hasOwnProperty(icon)) {
					if (name.toLowerCase() in awesome[icon].svg) {
						const normalized = normalizeIcon(icon);
						cs += write(3, `${normalized} = '\\u${awesome[icon].unicode}',`);
					}
				}
			}

			cs += write(2,"}");

			return cs;
		}

		function buildFile() {
			console.info("Build File");
			let content = fileHeader();
			content += buildEnum("Brands") + linebreak;
			content += buildEnum("Regular") + linebreak;
			content += buildEnum("Solid") + linebreak;
			content += fileBottom();
			return content;
		}

		function saveFile(content) {
			console.info("Save File");
			fs.writeFile(targetFile, content, e => {
				if (e) {
					onError(e);
				}
			});
		}

		if (!err) {
			const cs = buildFile();
			saveFile(cs);
		} else {
			onError(e);
		}
	});
}


GenerateIcons(ClassName, Source, Target);