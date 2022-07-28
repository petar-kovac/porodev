import axios from 'axios';
import { useFetchData } from 'hooks/useFetchData';
import {
  Dispatch,
  ReactNode,
  SetStateAction,
  useEffect,
  useState,
} from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  formatDate,
  millisToMinutesAndSeconds,
} from 'util/helpers/date-formaters';
import { IFilesCard } from 'types/card-data';
import { Image } from 'antd';
import styled from 'styled-components';
import { usePageContext } from 'context/PageContext';
import PList from 'components/list/List';
import SharedSpaceList from 'components/list/SharedSpaceList';

interface ISelectProps {
  fileName: string;
  fileId: string;
  uploadTime: string;
}

export const AddFileToSharedSpace = (
  data: any,
  cardData: any,
  setIsSiderModalVisible: any,
): ReactNode => {
  return (
    <SharedSpaceList
      data={data}
      cardData={cardData}
      setIsSiderModalVisible={setIsSiderModalVisible}
    />
  );
};
