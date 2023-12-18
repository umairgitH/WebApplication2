﻿/*
This file is the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. https://go.microsoft.com/fwlink/?LinkId=518007
*/

//var gulp = require('gulp');




//gulp.task('default', function () {
//    // place code for your default task here
//});

let gulp = require('gulp');
let ts = require('gulp-typescript');
let tsProject = ts.createProject("tsconfig.json");
gulp.task('ts-default', function () {
    return tsProject.src().pipe(tsProject()).js.pipe(gulp.dest("dist"));
});
gulp.task("watch:ts-default", async function () {
    gulp.watch("src/Widget/*.ts", gulp.series("ts-default"));
});