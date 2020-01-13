import '@babel/polyfill';
import '@ptrampert/flatitude';
import App from './App.jsx';
import ReactDOM from 'react-dom';
import React from 'react';

const e = React.createElement;

let container = document.createElement('div');
document.body.style.height = '100%';
document.body.append(container);
ReactDOM.render(e(App), container);