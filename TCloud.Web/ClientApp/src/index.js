import '@babel/polyfill';
import '@ptrampert/flatitude';
import App from './App.jsx';
import ReactDOM from 'react-dom';
import React from 'react';

const e = React.createElement;

let container = document.createElement('div');
container.style.height = '100vh';
document.body.append(container);
ReactDOM.render(e(App), container);