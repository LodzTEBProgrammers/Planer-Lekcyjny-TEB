import React from "react"
import ReactDOM from "react-dom/client"
import "./index.css"

import { createBrowserRouter, RouterProvider } from "react-router-dom"
import Root from "./pages/Root"
import ErrorPage from "./pages/NoPage"
import Test from "./pages/Test"
import { AnnouncementsProvider } from "./hooks/useAnnouncements"

const router = createBrowserRouter([
	{
		path: "/",
		element: <Root />,
		errorElement: <ErrorPage />,
		children: [
			{
				path: "chuj",
				element: (
					<AnnouncementsProvider>
						<Test />
					</AnnouncementsProvider>
				),
			},
		],
	},
])

ReactDOM.createRoot(document.getElementById("root")).render(
	<React.StrictMode>
		<RouterProvider router={router} />
	</React.StrictMode>
)
