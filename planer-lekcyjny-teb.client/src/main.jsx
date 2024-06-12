import './index.css'

import React from 'react'
import ReactDOM from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'

import { AnnouncementsProvider } from './hooks/useAnnouncements'
import ErrorPage from './pages/NoPage'
import Root from './pages/Root'
import Test from './pages/Test'

const router = createBrowserRouter([
	{
		path: '/',
		element: <Root />,
		errorElement: <ErrorPage />,
		children: [
			{
				path: 'chuj',
				element: (
					<AnnouncementsProvider>
						<Test />
					</AnnouncementsProvider>
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
