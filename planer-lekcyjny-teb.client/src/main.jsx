import './index.css'

import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'

import { AuthProvider } from './hooks/authContext'
import NoPage from './pages/NoPage/NoPage'
import Root from './pages/Root'
import Test from './pages/Test'


const router = createBrowserRouter([
  {
    path: '/',
    element: <Root />,
    errorElement: <NoPage/>,
    children: [
      {
        path: 'chuje',
        element: (
          <AuthProvider>
            <Test />
          </AuthProvider>
        ),
      },
    ],
  },
])

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router} />
  </React.StrictMode>
)
