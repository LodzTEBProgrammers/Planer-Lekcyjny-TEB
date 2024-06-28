import { useAuth } from '../hooks/useAuth'

const Test = () => {
  const { register } = useAuth()

  return (
    <>
      <h1 onClick={() => register('test', 'ciapibara@gmail.com', 'chujemujeDzikieW3ze>', 'Ciapibara')}>chujj</h1>
      <h2>chuj2</h2>
    </>
  )
}

export default Test
