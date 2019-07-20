'use strict';
var port = process.env.PORT || 1337;
var express = require('express');
var app = express();

app.get('/', function (req, res) {
    res.send('hello world');
});

app.listen(port);