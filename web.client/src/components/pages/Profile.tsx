import { useSelector } from 'react-redux'
import { RootState } from '../../redux/store'
import { DisplayLabel } from '../atoms/texts/DisplayLabel'
import { PageBase } from 'components/layout/pages/PageBase'

type Props = {}

export const Profile = (props: Props) => {
  const user = useSelector((state: RootState) => state.user)

  return (
    <PageBase>
      <h1>{user.stageName}</h1>
      <DisplayLabel header="Last name" text={user.lastName} />
    </PageBase>
  )
}