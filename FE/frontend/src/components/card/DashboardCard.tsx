import { FC } from 'react';
import styled from 'styled-components';
import { StyledDashboardCard } from './card-styled';

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

export default DashboardCard;
