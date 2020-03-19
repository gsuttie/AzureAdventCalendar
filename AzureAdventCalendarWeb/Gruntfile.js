/// <binding AfterBuild='default' />
const sass = require('node-sass');

module.exports = function (grunt) {
    'use strict';

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        sass: {
            options: {
                sourceMap: true,
                outputStyle: 'compressed',
                implementation: sass
            },
            dist: {
                files: [
                    {
                        expand: true,
                        cwd: "Styles",
                        src: ["**/*.scss"],
                        dest: "wwwroot/css",
                        ext: ".css"
                    }
                ]
            }
        }
    });

    grunt.loadNpmTasks('grunt-sass');
    grunt.registerTask('default', ['sass']);
};
