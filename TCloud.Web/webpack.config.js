const TerserJsPlugin = require('terser-webpack-plugin');
const OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const path = require('path');
const devMode = process.env.NODE_ENV !== 'production';

module.exports = {
  devtool: 'source-map',
  mode: devMode ? 'development' : 'production',
  entry: {
    index: './scripts/index.js'
  },
  output: {
    path: path.join(__dirname, 'wwwroot/'),
    filename: '[name].js'
  },
  optimization: {
    minimizer: [
      new TerserJsPlugin({
        sourceMap: true
      }),
      new OptimizeCssAssetsPlugin({})
    ]
  },
  module: {
    rules: [
      {
        enforce: 'pre',
        test: /\.jsx?$/,
        use: 'eslint-loader',
        exclude: /node_modules/
      },
      {
        test: /\.jsx?$/,
        use: 'babel-loader'
      },
      {
        test: /\.css$/,
        exclude: /node_modules/,
        use: [
          devMode ? 'style-loader' : MiniCssExtractPlugin.loader,
          {
            loader: 'css-loader',
            options: {
              modules: 'local',
              sourceMap: true,
              localsConvention: 'camelCase'
            }
          }
        ]
      },
      {
        test: /\.css$/,
        exclude: /scripts/,
        use: [
          devMode ? 'style-loader' : MiniCssExtractPlugin.loader,
          {
            loader: 'css-loader',
            options: {
              modules: false,
              sourceMap: true,
              localsConvention: 'camelCase'
            }
          }
        ]
      },
      {
        test: /\.(eot|svg|ttf|woff|woff2|png)/,
        use: {
          loader: 'file-loader',
          options: {
            name: '[name].[ext]'
          }
        }
      }
    ]
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: '[name].css'
    }),
    new HtmlWebpackPlugin({
      title: "TCloud",
      hash: true
    })
  ]
};