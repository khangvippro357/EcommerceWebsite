import React from 'react'
import { Navigate, useRoutes } from 'react-router-dom'
import Dashboard from './components/Dashboard/Index'
import CategoryList from './pages/AdminPages/Category'
import AddCategory from './pages/AdminPages/Category/AddCategory'
import Home from './pages/Home'
import ProductList from './pages/Product'
import AddProduct from './pages/Product/AddProduct'

export default function Router() {
  return useRoutes([
    {
      path: '/',
      element: <Navigate to="/admin/dashboard" />,
    },
    {
      path: '/admin',
      element: <Dashboard />,
      children: [
        {
          path: 'dashboard',
          element: <Home />,
        },
        {
          path: "product",
          element: <ProductList/>
        },
        {
          path: "category",
          element: <CategoryList/>
        }
      ],
    },
    {
      path: "/admin/product",
      element: <Dashboard/>,
      children: [
        {
          path: 'create',
          element: <AddProduct />,
        },
       
      ]
    },
    {
      path: "/admin/category",
      element: <Dashboard/>,
      children: [
        {
          path: 'create',
          element: <AddCategory />,
        },
       
      ]
    }
    
  ])
}
