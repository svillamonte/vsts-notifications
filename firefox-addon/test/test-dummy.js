var tests = require("sdk/test");

exports["test dummy"] = function(assert) {
  assert.ok(true, "Dummy assert");
}

tests.run(exports);
