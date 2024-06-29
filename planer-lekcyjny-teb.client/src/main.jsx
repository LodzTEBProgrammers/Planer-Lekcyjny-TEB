import './index.css'

import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'

import { AuthProvider } from './hooks/authContext'
import { NoPage, Root, Test } from './pages/pages'

const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    errorElement: <NoPage />,
    children: [
      {
        path: 'chuje',
        element: <Test />,
      },
    ],
  },
])

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <AuthProvider>
      <RouterProvider router={router} />
    </AuthProvider>
  </React.StrictMode>
)
