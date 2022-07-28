import { downloadFile, deleteFile } from 'service/files/files';
import { deleteFileFromSharedSpace } from 'service/shared-spaces/shared-spaces';

export const handleDownload = async (fileId: any, fileName: any) => {
  const res = await downloadFile(fileId);
  const url = window.URL.createObjectURL(new Blob([res.data]));

  const link = document.createElement('a');
  link.href = url;
  link.setAttribute('download', `${fileName}`);
  document.body.appendChild(link);
  link.click();

  link.parentNode?.removeChild(link);
};

export const handleDelete = async (
  fileId: any,
  isSharedSpaceFile: any,
  sharedSpaceId: any,
) => {
  if (isSharedSpaceFile) {
    await deleteFileFromSharedSpace({ sharedSpaceId, fileId });
  } else {
    await deleteFile(fileId);
  }
  const listCard = document.getElementById('remove-id');
  listCard?.parentNode?.removeChild(listCard);
};
