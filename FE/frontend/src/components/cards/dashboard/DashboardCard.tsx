import { FC } from 'react';
import styled from 'styled-components';

interface IDashboardCardProps {
  title: string;
  numberOfAdmins: number;
  numberOfFiles: number;
}

const DashboardCard: FC<IDashboardCardProps> = ({
  title,
  numberOfAdmins,
  numberOfFiles,
}) => {
  const capitalizedTitle = title.charAt(0).toUpperCase() + title.slice(1);

  return (
    <StyledDashboardCard>
      <h3>{capitalizedTitle}</h3>
      <p>Number of admins: {numberOfAdmins}</p>
      <p>Number of files: {numberOfFiles}</p>
    </StyledDashboardCard>
  );
};
const StyledDashboardCard = styled.div`
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 2rem;
  padding: 2rem;
  min-width: 22rem;
  flex-grow: 1;
`;

export default DashboardCard;
